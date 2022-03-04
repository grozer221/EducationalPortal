import React, {FC, useState} from 'react';
import {Card, message, Pagination, Space} from 'antd';
import {ButtonsVUR} from '../../../../../../components/ButtonsVUD/ButtonsVUR';
import {ButtonCreate} from '../../../../../../components/ButtonCreate/ButtonCreate';
import {SubjectPostsCreate} from '../SubjectPostsCreate/SubjectPostsCreate';
import {Subject} from '../../../subjects/subjects.types';
import {useMutation} from '@apollo/client';
import {REMOVE_SUBJECT_POST_MUTATION, RemoveSubjectPostData, RemoveSubjectPostVars} from '../../subjectPosts.mutations';
import {SubjectPostsUpdate} from '../SubjectPostsUpdate/SubjectPostsUpdate';
import {SubjectPost} from '../../subjectPosts.types';

type Props = {
    subject: Subject,
    refetchSubjectAsync: () => void,
    postsPage: number,
    setPostsPage: (page: number) => void,
};

export const SubjectPostsIndex: FC<Props> = ({subject, refetchSubjectAsync, postsPage, setPostsPage}) => {
    const [isModalPostCreateVisible, setIsModalPostCreateVisible] = useState(false);
    const [isModalPostUpdateVisible, setIsModalPostUpdateVisible] = useState(false);
    const [inEditingPost, setInEditingPost] = useState<SubjectPost | null>(null);
    const [removeSubjectPostMutation, removeSubjectPostMutationOptions] = useMutation<RemoveSubjectPostData, RemoveSubjectPostVars>(REMOVE_SUBJECT_POST_MUTATION);

    const onPostRemove = (postId: string) => {
        removeSubjectPostMutation({
            variables: {
                id: postId,
            },
        })
            .then(async (response) => {
                await refetchSubjectAsync();
            })
            .catch(e => {
                message.error(e.message);
            });
    };

    const onPostUpdate = (post: SubjectPost) => {
        setInEditingPost(post);
        setIsModalPostUpdateVisible(true);
    };

    return (
        <>
            <Space direction={'vertical'} style={{width: '100%'}} size={20}>
                <span onClick={() => setIsModalPostCreateVisible(true)}>
                    <ButtonCreate>Створити пост</ButtonCreate>
                </span>
                {subject.posts.entities.map(post => (
                    <Card
                        key={post.id}
                        type={'inner'}
                        title={<><strong>{post.title}</strong> {post.type}</>}
                        extra={
                            <ButtonsVUR
                                onUpdate={() => onPostUpdate(post)}
                                onRemove={() => onPostRemove(post.id)}
                            />
                        }
                    >
                        {post.text}
                    </Card>
                ))}
                {subject?.posts?.total > 0 &&
                <Pagination defaultCurrent={postsPage} onChange={setPostsPage} total={subject?.posts.total}/>}
            </Space>
            <SubjectPostsCreate
                isModalPostCreateVisible={isModalPostCreateVisible}
                setIsModalPostCreateVisible={setIsModalPostCreateVisible}
                subjectId={subject.id}
                refetchSubjectAsync={refetchSubjectAsync}
            />
            {inEditingPost && <SubjectPostsUpdate
                isModalPostUpdateVisible={isModalPostUpdateVisible}
                setIsModalPostUpdateVisible={setIsModalPostUpdateVisible}
                refetchSubjectAsync={refetchSubjectAsync}
                inEditingPost={inEditingPost}
                setInEditingPost={setInEditingPost}
            />}
        </>
    );
};
