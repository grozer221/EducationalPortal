import React, {useEffect, useState} from 'react';
import {useMutation, useQuery} from '@apollo/client';
import {ColumnsType} from 'antd/es/table';
import {ButtonsVUR} from '../../../../../../components/ButtonsVUD/ButtonsVUR';
import {message, Space, Table, Tag} from 'antd';
import {ButtonCreate} from '../../../../../../components/ButtonCreate/ButtonCreate';
import {Link} from 'react-router-dom';
import {GET_SUBJECTS_QUERY, GetSubjectsData, GetSubjectsVars} from '../../subjects.queries';
import {Subject} from '../../subjects.types';
import {REMOVE_SUBJECT_MUTATION, RemoveSubjectData, RemoveSubjectVars} from '../../subjects.mutations';
import Title from 'antd/es/typography/Title';
import {useAppSelector} from '../../../../../../store/store';

export const SubjectsIndex = () => {
    const currentUser = useAppSelector(s => s.auth.me?.user);
    const [page, setPage] = useState(1);
    const getSubjectsQuery = useQuery<GetSubjectsData, GetSubjectsVars>(GET_SUBJECTS_QUERY,
        {variables: {page: page}, fetchPolicy: 'network-only'},
    );
    const [removeSubjectMutation, removeSubjectMutationOptions] = useMutation<RemoveSubjectData, RemoveSubjectVars>(REMOVE_SUBJECT_MUTATION);

    useEffect(() => {
        getSubjectsQuery.refetch({page: page});
    }, [page]);

    const onRemove = (subjectId: string) => {
        removeSubjectMutation({variables: {id: subjectId}})
            .then(async (response) => {
                await getSubjectsQuery.refetch({page});
            })
            .catch(error => {
                message.error(error.message);
            });
    };

    const columns: ColumnsType<Subject> = [
        {
            title: 'Назва',
            dataIndex: 'name',
            key: 'name',
            render: (text, subject) => (
                <Space>
                    <div>{subject?.name}</div>
                    <div>
                        {subject.teacherId === currentUser?.id && <Tag color={'green'}>Мій</Tag>}
                        {subject.teachersHaveAccessCreatePosts.some(t => t.id === currentUser?.id) &&
                        <Tag color={'cyan'}>Надано доступ</Tag>}
                    </div>
                </Space>
            ),
        },
        {
            title: 'Вчитель',
            dataIndex: 'teacher',
            key: 'teacher',
            render: (text, subject) => (
                <Link to={`../../teachers/${subject?.teacherId}`}>
                    {subject?.teacher?.lastName} {subject?.teacher?.firstName}
                </Link>
            ),
        },
        {
            title: 'Навчальний рік',
            dataIndex: 'educationalYear',
            key: 'educationalYear',
            render: (text, subject) => <Link
                to={`../../educational-years/${subject?.educationalYearId}`}>{subject?.educationalYear?.name}</Link>,
        },
        {
            title: 'Дії',
            dataIndex: 'actions',
            key: 'actions',
            render: (text: string, subject: Subject) => (
                // (currentUser?.id === subject.teacherId || currentUser?.role === Role.Administrator)
                //     ? <ButtonsVUR viewUrl={`${subject?.id}`} updateUrl={`update/${subject?.id}`}
                //                   onRemove={() => onRemove(subject?.id)}/>
                //     : <ButtonsVUR viewUrl={`${subject?.id}`}/>
                <ButtonsVUR viewUrl={`${subject?.id}`} updateUrl={`update/${subject?.id}`}
                            onRemove={() => onRemove(subject?.id)}/>
            ),
        },
    ];

    return (
        <Space size={20} direction={'vertical'} style={{width: '100%'}}>
            <Title level={2}>Предмети</Title>
            <Link to={'create'}>
                <ButtonCreate/>
            </Link>
            <Table
                rowKey={'id'}
                loading={getSubjectsQuery.loading || removeSubjectMutationOptions.loading}
                dataSource={getSubjectsQuery.data?.getSubjects.entities}
                columns={columns}
                pagination={{
                    total: getSubjectsQuery.data?.getSubjects.total,
                    onChange: setPage,
                }}
            />
        </Space>
    );
};
