import React, {useCallback, useEffect, useState} from 'react';
import {useLazyQuery, useMutation} from '@apollo/client';
import {ColumnsType} from 'antd/es/table';
import {ButtonsVUR} from '../../../../../../components/ButtonsVUD/ButtonsVUR';
import {Col, message, Row, Space, Table} from 'antd';
import {ButtonCreate} from '../../../../../../components/ButtonCreate/ButtonCreate';
import {Link, useSearchParams} from 'react-router-dom';
import {GET_USERS_QUERY, GetUsersData, GetUsersVars} from '../../users.queries';
import {Role, User} from '../../users.types';
import {REMOVE_USER_MUTATION, RemoveUserData, RemoveUserVars} from '../../users.mutations';
import Title from 'antd/es/typography/Title';
import {roleToTag} from '../../../../../../convertors/toTagConvertor';
import debounce from 'lodash.debounce';
import Search from 'antd/es/input/Search';

export const TeachersIndex = () => {
    const [searchParams, setSearchParams] = useSearchParams();
    const [likeInput, setLikeInput] = useState('');
    const [roles, setRoles] = useState([Role.Teacher, Role.Administrator]);
    const [getTeachers, getTeachersOptions] = useLazyQuery<GetUsersData, GetUsersVars>(GET_USERS_QUERY,
        {fetchPolicy: 'network-only'},
    );
    const [removeTeacherMutation, removeTeacherMutationOptions] = useMutation<RemoveUserData, RemoveUserVars>(REMOVE_USER_MUTATION);

    useEffect(() => {
        const page = parseInt(searchParams.get('page') || '') || 1;
        const like = searchParams.get('like') || '';
        setLikeInput(like);
        getTeachers({variables: {page, like, roles}});
    }, [searchParams]);

    const onRemove = (studentId: string) => {
        removeTeacherMutation({variables: {id: studentId}})
            .then(async (response) => {
                const page = parseInt(searchParams.get('page') || '') || 1;
                const like = searchParams.get('like') || '';
                await getTeachers({variables: {page, like, roles}});
            })
            .catch(error => {
                message.error(error.message);
            });
    };

    const columns: ColumnsType<User> = [
        {
            title: 'Учень',
            dataIndex: 'student',
            key: 'student',
            render: (text, student) => <>{student?.lastName} {student?.firstName}</>,
        },
        {
            title: 'Роль',
            dataIndex: 'role',
            key: 'role',
            render: (text, student) => <>{student?.role && roleToTag(student.role)}</>,
        },
        {
            title: 'Дії',
            dataIndex: 'actions',
            key: 'actions',
            render: (text: string, student) => (
                // isAdministrator()
                //     ? <ButtonsVUR viewUrl={`${student?.id}`} updateUrl={`update/${student?.id}`}
                //                   onRemove={() => onRemove(student?.id)}/>
                //     : <ButtonsVUR viewUrl={`${student?.id}`}/>
                <ButtonsVUR viewUrl={`${student?.id}`} updateUrl={`update/${student?.id}`}
                            onRemove={() => onRemove(student?.id)}/>
            ),
        },
    ];

    const debouncedSearchTeachersHandler = useCallback(debounce(like => setSearchParams({like}), 500), []);
    const searchTeachersHandler = (value: string) => {
        debouncedSearchTeachersHandler(value);
        setLikeInput(value);
    };

    return (
        <Space size={20} direction={'vertical'} style={{width: '100%'}}>
            <Title level={2}>Вчителі</Title>
            {/*{isAdministrator() &&*/}
            {/*<Link to={'create'}>*/}
            {/*    <ButtonCreate/>*/}
            {/*</Link>*/}
            {/*}*/}
            <Row justify="space-between">
                <Col>
                    <Link to={'create'}>
                        <ButtonCreate/>
                    </Link>
                </Col>
                <Col>
                    <Search
                        allowClear
                        value={likeInput}
                        onChange={e => searchTeachersHandler(e.target.value)}
                        placeholder="Пошук"
                        enterButton
                        loading={getTeachersOptions.loading}
                        className={'search'}
                    />
                </Col>
            </Row>
            <Table
                rowKey={'id'}
                loading={getTeachersOptions.loading || removeTeacherMutationOptions.loading}
                dataSource={getTeachersOptions.data?.getUsers.entities}
                columns={columns}
                pagination={{
                    total: getTeachersOptions.data?.getUsers.total,
                    onChange: page => setSearchParams({page: page.toString()}),
                }}
            />
        </Space>
    );
};