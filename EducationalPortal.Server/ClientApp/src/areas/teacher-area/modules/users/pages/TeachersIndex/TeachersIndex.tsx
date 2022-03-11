import React, {useEffect, useState} from 'react';
import {useLazyQuery, useMutation} from '@apollo/client';
import {ColumnsType} from 'antd/es/table';
import {ButtonsVUR} from '../../../../../../components/ButtonsVUD/ButtonsVUR';
import {message, Space, Table} from 'antd';
import {ButtonCreate} from '../../../../../../components/ButtonCreate/ButtonCreate';
import {Link} from 'react-router-dom';
import {GET_USERS_QUERY, GetUsersData, GetUsersVars} from '../../users.queries';
import {Role, User} from '../../users.types';
import {REMOVE_USER_MUTATION, RemoveUserData, RemoveUserVars} from '../../users.mutations';
import Title from 'antd/es/typography/Title';
import {isAdministrator} from '../../../../../../utils/permissions';
import {roleToTag} from '../../../../../../convertors/toTagConvertor';

export const TeachersIndex = () => {
    const [page, setPage] = useState(1);
    const [roles, setRoles] = useState([Role.Teacher, Role.Administrator]);
    const [like, setLike] = useState('');
    const [getTeachers, getTeachersOptions] = useLazyQuery<GetUsersData, GetUsersVars>(GET_USERS_QUERY,
        {variables: {page: page, roles: roles, like: like}, fetchPolicy: 'network-only'},
    );
    const [removeTeacherMutation, removeTeacherMutationOptions] = useMutation<RemoveUserData, RemoveUserVars>(REMOVE_USER_MUTATION);

    useEffect(() => {
        getTeachers({variables: {page, roles, like}});
    }, [page, roles]);

    const onRemove = (studentId: string) => {
        removeTeacherMutation({variables: {id: studentId}})
            .then(async (response) => {
                await getTeachers({variables: {page, roles, like}});
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

    return (
        <Space size={20} direction={'vertical'} style={{width: '100%'}}>
            <Title level={2}>Вчителі</Title>
            {/*{isAdministrator() &&*/}
            {/*<Link to={'create'}>*/}
            {/*    <ButtonCreate/>*/}
            {/*</Link>*/}
            {/*}*/}
            <Link to={'create'}>
                <ButtonCreate/>
            </Link>
            <Table
                rowKey={'id'}
                loading={getTeachersOptions.loading || removeTeacherMutationOptions.loading}
                dataSource={getTeachersOptions.data?.getUsers.entities}
                columns={columns}
                pagination={{
                    total: getTeachersOptions.data?.getUsers.total,
                    onChange: setPage,
                }}
            />
        </Space>
    );
};
